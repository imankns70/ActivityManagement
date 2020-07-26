import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/components/auth/services/auth.service';

@Component({
  selector: 'app-change-pic',
  templateUrl: '../change-pic/change-pic.component.html',
  styleUrls: ['../change-pic/change-pic.component.css']
})
export class ChangePicComponent implements OnInit {
  uploader: FileUploader;
  hasBaseDropZoneOver: false;
  // hasAnotherDropZoneOver: false;
  // response: string;
  baseUrl = environment.apiUrl;
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.initializeUploader()
  }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }


  initializeUploader() {

    this.uploader = new FileUploader({
      url: this.baseUrl + 'UserManager/ChangeUserPhoto',
      //authToken: 'Berear' + localStorage.getItem('token'),
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
      queueLimit: 1,
      disableMultipart: true, // 'DisableMultipart' must be 'true' for formatDataFunction to be called.
      formatDataFunctionIsAsync: true,
      formatDataFunction: async (item) => {
        return new Promise((resolve, reject) => {
          debugger;
          resolve({

            name: item._file.name,
            length: item._file.size,
            contentType: item._file.type,
            date: new Date()


          });
        });
      },

      headers: [
        { name: 'Content-Type', value: 'application/json' },
        { name: 'Authorization', value: 'Bearer ' + localStorage.getItem('token') }
      ],


    })
    // this.hasBaseDropZoneOver = false;
    // this.hasAnotherDropZoneOver = false;

    // this.response = '';
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false }

    // this.uploader.response.subscribe(res => this.response = res);
  }

}
