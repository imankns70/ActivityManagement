import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';
import { SharedService } from 'src/app/Services/Shared.service';

@Component({
  selector: 'app-change-pic',
  templateUrl: '../change-pic/change-pic.component.html',
  styleUrls: ['../change-pic/change-pic.component.css']
})
export class ChangePicComponent implements OnInit {

  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  response: string;
  baseUrl = environment.apiUrl;
  constructor(private sharedService: SharedService) { }

  ngOnInit() {
    this.initializeUploader()
    console.log(this.response)
  }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }


  initializeUploader() {

    this.uploader = new FileUploader({
      url: this.baseUrl + 'UserManager/ChangeUserPhoto',
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
      queueLimit: 1,
      headers: [
        { name: 'Authorization', value: 'Bearer ' + localStorage.getItem('token') }
      ],


    })


    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false }

    this.uploader.onSuccessItem = (item, response, status, Headers) => {

      debugger;
      if (response) {
       
        this.sharedService.setUserPhoto(response)
      }
    }
  }

}
