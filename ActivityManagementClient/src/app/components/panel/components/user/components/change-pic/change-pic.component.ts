import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';
import { ApiResult } from 'src/app/models/apiresult';
import { AuthService } from 'src/app/Shared/Services/auth/services/auth.service';
import { Store } from '@ngrx/store';
import * as fromStore from 'src/app/store'
import { EditLoggedUser } from 'src/app/store';
@Component({
  selector: 'app-change-pic',
  templateUrl: '../change-pic/change-pic.component.html',
  styleUrls: ['../change-pic/change-pic.component.css']
})
export class ChangePicComponent implements OnInit {

  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  imageUrl: string;
  baseUrl = environment.apiUrl;
  constructor(private authService: AuthService,
    private generalStore: Store<fromStore.State>) { }

  ngOnInit() {
    this.initializeUploader()
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

      let apiResult: ApiResult
      
      apiResult = <ApiResult>JSON.parse(response);

      if (apiResult.isSuccess) {

         this.generalStore.dispatch(new fromStore.EditLoggedUserPhotoUrl(apiResult.data));            

      }
    }
  }

}
