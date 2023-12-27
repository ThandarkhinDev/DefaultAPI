import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FileuploadRoutingModule } from './fileupload-routing.module';
import { FileuploadComponent } from './fileupload.component';
import { UploadModule } from '@progress/kendo-angular-upload';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FileuploadRoutingModule,
    UploadModule,
    FormsModule
  ],
  declarations: [FileuploadComponent]
})
export class FileuploadModule { }
