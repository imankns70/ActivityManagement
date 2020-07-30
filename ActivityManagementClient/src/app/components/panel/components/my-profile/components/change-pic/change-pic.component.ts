import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/components/auth/services/auth.service';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-change-pic',
  templateUrl: './change-pic.component.html',
  styleUrls: ['./change-pic.component.css']
})
export class ChangePicComponent implements OnInit {
  @Output() getUserImageUrl= new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver:boolean;
  response:string;
  baseUrl = environment.apiUrl;
  constructor() { }

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

    this.uploader.onSuccessItem= (item,response,status,Headers) => {

      if(response){
        this.getUserImageUrl.emit(response)
      }
    }
  }

}
