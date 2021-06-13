import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from 'src/app/models/user/user';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.scss']
})
export class DeleteUserComponent implements OnInit {

  constructor() { }
  private _user: User
  public active = false;
  @Input() public set model(user: User) {
    this._user = user;
    this.active = typeof user !== "undefined"
  }

  @Output() delete: EventEmitter<User> = new EventEmitter();
  @Output() cancel: EventEmitter<User> = new EventEmitter();

  ngOnInit() {

  }
  get userInfo(): string {
    return `آیا از حذف  ${this._user.firstName}  ${this._user.lastName} مطمئن هستید ؟`;

  }

  onDelete() {

    this.delete.emit(this._user);
    this.active = false;
  }

  closeForm() {
    this.cancel.emit();
    this.active = false;

  }

}
