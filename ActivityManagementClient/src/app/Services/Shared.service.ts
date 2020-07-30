import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  userPhoto = "../../assets/images/avatars/avatar-1.jpg";
  imageUrl: BehaviorSubject<string>
  constructor() {

    this.imageUrl = new BehaviorSubject(this.userPhoto)
  
  }

  setUserPhoto(url:string){

    this.imageUrl.next(url);
  }
}
