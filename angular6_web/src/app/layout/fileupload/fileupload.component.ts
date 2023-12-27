import { Component, OnInit } from '@angular/core';
import { FileInfo, FileRestrictions, ClearEvent, RemoveEvent, SelectEvent, UploadEvent, FileState } from '@progress/kendo-angular-upload';
import { FileuploadService } from '../../core/services/fileupload.service';

@Component({
  selector: 'app-fileupload',
  templateUrl: './fileupload.component.html',
  styleUrls: ['./fileupload.component.scss']
})
export class FileuploadComponent {
  public state;
  public userImages: Array<FileInfo>;
  public events: string[] = [];
  public imagePreviews: FileInfo[] = [];
  public uploadRestrictions: FileRestrictions = {
    allowedExtensions: ['.jpg', '.png']
  };

  public uploadSaveUrl = ''; // should represent an actual API endpoint
  public uploadRemoveUrl = 'removeUrl'; // should represent an actual API endpoint


  constructor(private fileuplaod: FileuploadService) {}
  public clearEventHandler(e: ClearEvent): void {
    // this.log('Clearing the file upload');
    this.imagePreviews = [];
  }

  public completeEventHandler() {
    // this.log(`All files processed`);
  }

  public removeEventHandler(e: RemoveEvent): void {
    const index = this.imagePreviews.findIndex(item => item.uid === e.files[0].uid);

    if (index >= 0) {
      this.imagePreviews.splice(index, 1);
    }
  }

  public selectEventHandler(e: SelectEvent): void {
    const that = this;

    e.files.forEach((file) => {
      if (!file.validationErrors) {
        const reader = new FileReader();

        reader.onload = function (ev) {
          const tmpimage = <FileReader>ev.target;
          const image = {
            src: tmpimage.result,
            uid: file.uid,
            name: ''
          };
          that.imagePreviews.unshift(image);
        };

        reader.readAsDataURL(file.rawFile);
      }
    });
  }

  public base64ToBlob(base64Data, contentType) {
    contentType = contentType || '';
    const sliceSize = 1024;
    const byteCharacters = atob(base64Data);
    const bytesLength = byteCharacters.length;
    const slicesCount = Math.ceil(bytesLength / sliceSize);
    const byteArrays = new Array(slicesCount);

    for (let sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
      const begin = sliceIndex * sliceSize;
      const end = Math.min(begin + sliceSize, bytesLength);
      const bytes = new Array(end - begin);
      for (let offset = begin, i = 0 ; offset < end; ++i, ++offset) {
        bytes[i] = byteCharacters[offset].charCodeAt(0);
      }
      byteArrays[sliceIndex] = new Uint8Array(bytes);
    } 
    return new Blob(byteArrays, { type: contentType });
  }

  uploadEventHandler(e: UploadEvent) {
    const a = this.userImages;
    const that = this;
    e.files.forEach((file) => {
       if (!file.validationErrors) {
        const reader = new FileReader();
        const params = 1 + '/' + file.extension;
        const encryptdata = btoa(params);

        reader.onload = function (ev) {
          const functionname = 'uploadAdminPhoto';
          const tmpimage = <FileReader>ev.target;
          const image = tmpimage.result.split(',');
         // const image_blob = that.base64ToBlob(tmpimage.result.replace('data:image/jpeg;base64,', ''), 'image/jpeg');
         const image_blob = that.base64ToBlob(image[1], 'image/jpeg'); 
         const formData = new FormData();
          formData.append( 'fileUpload', image_blob );
          that.fileuplaod.uploadFile(functionname, encryptdata, formData).subscribe( x => that.state = true);
        };
 
         reader.readAsDataURL(file.rawFile);
         e.preventDefault(); // to prevent default upload url
       }
     });
  }

  public remove(upload, uid: string) {
    const index = this.imagePreviews.findIndex(item => item.uid === uid);
    if (index >= 0) {
      this.imagePreviews.splice(index, 1);
    }
  }

}
