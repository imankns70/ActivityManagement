import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/components/auth/services/auth.service';
 

@Component({
  selector: 'app-change-pic',
  templateUrl: './change-pic.component.html',
  styleUrls: ['./change-pic.component.css']
})
export class ChangePicComponent implements OnInit {
  uploader: FileUploader;
  hasBaseDropZoneOver: false;
  // hasAnotherDropZoneOver: boolean;
  // response: string;
  baseUrl = environment.apiUrl;
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'UserManager/ChangeUserPhoto',
      authToken: 'Berear' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: true,
      maxFileSize: 10 * 1024 * 1024

    })

  }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
}
