using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MFI;
using System.Linq;
using System.Text;
using MFI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;

namespace CustomTokenAuthProvider
{
    public class TokenProviderMiddleware: IMiddleware
    {
        private IRepositoryWrapper _repository;
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly JsonSerializerSettings _serializerSettings;
        public static IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
        }
        public TokenProviderMiddleware(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var appsettingbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var Configuration = appsettingbuilder.Build();
            double expiretimespan = Convert.ToDouble(Configuration.GetSection("TokenAuthentication:TokenExpire").Value);
            TimeSpan expiration = TimeSpan.FromMinutes(expiretimespan);
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));

            _options = new TokenProviderOptions
            {
                Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity,
                Expiration = expiration
            };
        }
        public async Task ResponseMessage(dynamic data, HttpContext context, int code = 400)
        {
            var response = new
            {
                status = data.status,
                message = data.message
            };
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));

        }
     
        private async Task GenerateToken(HttpContext context)
        {
           // var username = context.Request.Form["username"]; //admin
           // var password = context.Request.Form["password"]; //gwtsoft
           // var _loginType = context.Request.Form["LoginType"];
            LoginDataModel loginData = new LoginDataModel();
            string username = "";
            string password = "";
            string _loginType = "";
            try
            {
                using (var reader = new System.IO.StreamReader(context.Request.Body))
                {
                    var request_body = reader.ReadToEnd();
                    loginData = JsonConvert.DeserializeObject<LoginDataModel>(request_body , _serializerSettings);
                    if (loginData.username == null) loginData.username = "";
                    if (loginData.password == null) loginData.password = "";
                    if (loginData.LoginType == null) loginData.LoginType = "";
                    username = loginData.username;
                    password = loginData.password;
                    _loginType = loginData.LoginType;
                }
            }
            catch(Exception ex)
            {

            }

            string myhost = "";
            string ipaddress = "127.0.0.1";
            var hdmyhost = context.Request.Headers["Myhost"];
            string clienturl = context.Request.Headers["Referer"];

            if (hdmyhost.Count > 0)
            {
                myhost = hdmyhost[0];
            }

            dynamic result = null;
            if (_loginType == "1")
            {
                result = doAdminTypeloginValidation(username, password, clienturl, ipaddress);
            }

            if (result == null || result.Count <= 0)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            if (result[0].access_status == 1)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("This user account is deleted.");
                return;
            }

            if (result[0].access_status == 2)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("This user account is locked.");
                return;
            }

            string ipaddresslist = result[0].restricted_iplist;
            Boolean sameip = true;
            if (ipaddresslist != "" && ipaddresslist != null)
            {
                sameip = false;
                string[] ipaddressarr = ipaddresslist.Split(',');
                for (int ip_index = 0; ip_index < ipaddressarr.Length; ip_index++)
                {
                    if (ipaddress == ipaddressarr[ip_index].Trim())
                    {
                        sameip = true;
                        break;
                    }
                }
            }
            if (sameip == false)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Your IP Address is invalid for this account.");
                return;
            }

            string userID = result[0].AdminID.ToString();
            var now = DateTime.UtcNow;
            var _tokenData = new TokenData();
            _tokenData.Sub = result[0].AdminName;
            _tokenData.Jti = await _options.NonceGenerator();
            _tokenData.Iat = new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString();
            _tokenData.UserID = userID;
            _tokenData.Userlevelid = result[0].AdminLevelID.ToString();
            _tokenData.LoginType = _loginType.ToString();
            _tokenData.TicketExpireDate= now.Add(_options.Expiration);
            var claims = Globalfunction.GetClaims(_tokenData);

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var settingresult = (_repository.Setting.GetPasswordValidation()).ToList();
            var pwdlength = settingresult[0].Value;

            var response = new
            {data = 
                new { access_token = encodedJwt,
                    expires_in = (int)_options.Expiration.TotalSeconds,
                    UserID = userID,
                    LoginType = _loginType.ToString(),
                    userLevelID = result[0].AdminLevelID,
                    displayName = result[0].AdminName,
                    userImage = result[0].ImagePath,
                    PWDLength = pwdlength.ToString()
                    }
            };

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
        }

        dynamic doAdminTypeloginValidation(string username, string password, string clienturl, string ipaddress)
        {
            var result = (_repository.Admin.GetAdminLoginValidation(username)).ToList();

            if (result.Count <= 0)
            {
                return null;
            }
            //To set for Session Data
            string LoginUserID = result[0].AdminID.ToString();
            _session.SetString("LoginUserID", LoginUserID);
            _session.SetString("LoginRemoteIpAddress", ipaddress);
            _session.SetString("LoginTypeParam", "1");

            string oldhash = result[0].Password; //"wlaf1//SXWsJp/2+Mo8+1wnmxbmZ5ZAt";  //gwtsoft
            string oldsalt = result[0].Salt; //"/SApKtKXpIa6YnHCjKLxQJAeb279BlX8";
            bool flag = Operational.Encrypt.SaltedHash.Verify(oldsalt, oldhash, password);
            if (flag == false)
            {
               
                //increase login_failure count 
                var objAdmin = _repository.Admin.FindByID(result[0].AdminID);
                if (objAdmin != null)
                {
                    var newfailcount = result[0].login_fail_count + 1;
                    var settingresult = (_repository.Setting.GetAllowLoginFailCount()).ToList();
                    var settingfailcount = settingresult[0].Value;
                    objAdmin.login_fail_count = newfailcount;
                    //change access_status to 2 if login_failure_count = 'Allow Login Failure Count' from setting table
                    if (newfailcount.ToString() == settingfailcount)
                        objAdmin.access_status = 2;
                    _repository.Admin.UpdateAdmin(objAdmin);

                    //send email to unlock                 
                    // # write send unlock email code here
                }

                return null;
            }
            else
            {
                //reset login_failure count
                Admin objAdmin = _repository.Admin.FindByID(result[0].AdminID);
                if (objAdmin != null)
                {
                    objAdmin.login_fail_count = 0;
                    _repository.Admin.UpdateAdmin(objAdmin);
                    _repository.EventLog.InfoEventLog("TokenProviderMiddleWare", "Successful login for this account UserName : " + username + " , Password : " + password);
                }
            }

            return result;
        }
        private static void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Path))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Path));
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.Expiration));
            }

            if (options.IdentityResolver == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.IdentityResolver));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.SigningCredentials));
            }

            if (options.NonceGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.NonceGenerator));
            }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string ipaddress = "127.0.0.1";
            if (context.Connection.RemoteIpAddress != null) ipaddress = context.Connection.RemoteIpAddress.ToString();
            _session.SetString("LoginUserID", "0");
            _session.SetString("LoginRemoteIpAddress", ipaddress);
            _session.SetString("LoginTypeParam", "1");
           
            TokenData _tokenData = null;
            var access_token = "";
            var hdtoken = context.Request.Headers["Authorization"];
            if (hdtoken.Count > 0)
            {
                access_token = hdtoken[0];
                access_token = access_token.Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(access_token) as JwtSecurityToken;
                _tokenData = Globalfunction.GetTokenData(tokenS);
            }
            else
            {
                //TODO for some
            }
           //  _objdb = DB;
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                //Regenerate newtoken for not timeout at running
                string newToken = "";
                try
                {
                    var pathstr = context.Request.Path.ToString();
                    string[] patharr = pathstr.Split('/');
                    int prequest = Array.IndexOf(patharr, "PublicRequest");
                    int wrequest = Array.IndexOf(patharr, "WindowServiceRequest");
                    if (prequest < 1 && wrequest < 1)
                    {
                        var handler = new JwtSecurityTokenHandler();
                                    var tokenS = handler.ReadToken(access_token) as JwtSecurityToken;
                                    var allow = false;
                    
                        //check userlevel permission
                        if (patharr[1].ToString() == "api")
                        {
                            var objAdminLevel = _repository.AdminLevel.FindAdminLevel(int.Parse(_tokenData.Userlevelid));
                            var isadmin = false;
                            if (objAdminLevel != null)
                            {
                                isadmin = objAdminLevel.IsAdministrator;
                            }
                            if (isadmin || patharr[3] == "GetAdminLevelMenuData")
                                allow = true;
                            else
                            {
                            //string ipaddress = context.Connection.RemoteIpAddress.ToString();
                            // allow = checkURLPermission(_tokenData, patharr[2], patharr[3], ipaddress);
                            string controllername = patharr[2];
                                            string functionname = patharr[3];
                                            string ServiceUrl = controllername + "/" + functionname;
                                            AdminMenuUrl _AdminMenuUrl = _repository.AdminMenuUrl.GetAdminMenuUrlByServiceUrl(ServiceUrl);
                                            if (_AdminMenuUrl != null)
                                            {
                                                Adminlevelmenu _Adminlevelmenu = _repository.Adminlevelmenu.GetAdminlevelmenuByAdminLevelIDAdminMenuID(int.Parse(_tokenData.Userlevelid), _AdminMenuUrl.AdminMenuID);
                                                if (_Adminlevelmenu != null)
                                                {
                                                    allow = true;
                                                }

                                            }
                            }
                        }
                        allow = true;
                        if (allow)
                        {
                            // check token expired   
                            double expireTime = Convert.ToDouble(_options.Expiration.TotalMinutes);
                            DateTime issueDate = _tokenData.TicketExpireDate.AddMinutes(-expireTime);
                            DateTime NowDate = DateTime.UtcNow;
                            if (issueDate > NowDate || _tokenData.TicketExpireDate < NowDate)
                            {
                            // return "-2";
                            newToken = "-2";
                            }
                            // end of token expired check

                            var now = DateTime.UtcNow;
                            _tokenData.Jti = new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString();
                            _tokenData.Jti = await _options.NonceGenerator();

                            var claims = Globalfunction.GetClaims(_tokenData);
                            // Create the JWT and write it to a string
                            var jwt = new JwtSecurityToken(
                                issuer: _options.Issuer,
                                audience: _options.Audience,
                                claims: claims,
                                notBefore: now,
                                expires: now.Add(_options.Expiration),
                                signingCredentials: _options.SigningCredentials);
                            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                        //  return encodedJwt;
                        newToken = encodedJwt;
                        }
                        else
                            //return "-1";
                            newToken = "-1";
                    }
                }
                catch (Exception ex)
                {
                    Globalfunction.WriteSystemLog(ex.Message);
                }

                if (newToken == "-1")
                {
                    _repository.EventLog.InfoEventLog("TokenProviderMiddleWare", "Not include Authorization Header, Access Denied");
                    context.Response.StatusCode = 400;
                    await ResponseMessage(new { status = "fail", message = "Access Denied" }, context, 400);

                }
                if (newToken == "-2")
                {
                    context.Response.StatusCode = 400;
                   // return context.Response.WriteAsync("The Token has expired");
                    await ResponseMessage(new { status = "fail", message = "The Token has expired" }, context, 400);
                }

                if (newToken != "")
                {
                    context.Response.Headers.Add("Access-Control-Expose-Headers", "newToken");
                    context.Response.Headers.Add("newToken", newToken);
                }
               // return _next(context);
               await next(context);
            } else 
           // return GenerateToken(context);
           await GenerateToken(context);
        }
    }
}