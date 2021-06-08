import { Action } from '@ngrx/store'
import { User } from 'src/app/models/user/user';

export enum loggedIUserTypes {

    EDIT_LOGGEDUSER = '[Logged User] Edit',
    RESET_LOGGEDUSER = '[Logged User] Reset',
    EDIT_LOGGEDUSERPHOTOURL = '[Logged User PhotoUrl] Edit',
    EDIT_LOGGEDUSERFIRSTNAME = '[Logged User FirstName] Edit',

}

export class EditLoggedUser implements Action {
    readonly type = loggedIUserTypes.EDIT_LOGGEDUSER;
    constructor(public payload: User) { }

}

export class ResetLoggedUser implements Action {
    readonly type = loggedIUserTypes.RESET_LOGGEDUSER;


}

export class EditLoggedUserPhotoUrl implements Action {
    readonly type = loggedIUserTypes.EDIT_LOGGEDUSERPHOTOURL;
    constructor(public payload: string) { }

}

export class EditLoggedUserName implements Action {
    readonly type = loggedIUserTypes.EDIT_LOGGEDUSERFIRSTNAME;
    constructor(public payload: string) { }

}

export type All = EditLoggedUser | ResetLoggedUser | EditLoggedUserPhotoUrl | EditLoggedUserName
