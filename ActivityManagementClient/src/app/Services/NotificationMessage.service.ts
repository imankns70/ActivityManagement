import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Globals } from './Globals';

@Injectable({
  providedIn: 'root'
})
export class NotificationMessageService {

  constructor(private toastrAlert: ToastrService, private global: Globals) { }

  showMessage(textMessage: string, textTitle: string, textType: number) {
   
    if (textType == this.global.successMessage) {
      this.toastrAlert.success(textMessage, textTitle)
    } else if (textType == this.global.errorMessage) {
      this.toastrAlert.error(textMessage, textTitle)
    }
  }
}
