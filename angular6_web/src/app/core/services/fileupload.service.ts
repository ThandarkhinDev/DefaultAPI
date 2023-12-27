import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class FileuploadService {

  constructor(private apiservice: ApiService) { }
  uploadFile(functionname, encryptdata, formData) {
    return this.apiservice.uploadFile('/fileservice/Upload/' + functionname + '/' + encryptdata, formData);
  }

}
