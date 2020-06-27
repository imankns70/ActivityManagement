import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../models/enums/Globals';

@Injectable({
  providedIn: 'root'
})
export class NotificationMessageService {

  constructor(private toastrAlert: ToastrService) { }

  showMessage(textMessage: string, textTitle: string, textType: number) {
   
    if (textType == Globals.successMessage) {
      this.toastrAlert.success(textMessage, textTitle)
    } else if (textType == Globals.errorMessage) {
      this.toastrAlert.error(textMessage, textTitle)
    }
  }
}
