
using MFI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MFI
{
    [Route("api/[controller]")]
    public class SampleController: BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;
        public SampleController(IRepositoryWrapper RW) {
            _repositoryWrapper = RW;
        }

        [HttpGet("GetAllSamples", Name="GetAllSamples")]
        [Authorize]
        public dynamic GetAllSamples() {
            dynamic objresponse = null;
            var _allSample =   _repositoryWrapper.Sample.FindAll();
            return objresponse = new {data = _allSample};
        }

        [HttpPost("AddSample", Name="AddSample")]
        [Authorize]
        public dynamic AddSample([FromBody] Newtonsoft.Json.Linq.JObject param) {
            dynamic objresponse = null;
        /*     var objstr = HttpContext.Request.Form["SampleDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;
            int id = obj.ID;
            Sample _objSample = _repositoryWrapper.Sample.GetSampleById(id);
            if(_objSample != null) {
                _objSample.Name = obj.Name;
                _objSample.Description = obj.Description;
                _repositoryWrapper.Sample.Update(_objSample);
            }else {
                Sample _newObj = new Sample();
                _newObj.Name = obj.Name;
                _newObj.Description = obj.Description;
                _repositoryWrapper.Sample.Create(_newObj);
                id = _newObj.ID;
            }
            return objresponse = new { data = id };
        }
    }
}