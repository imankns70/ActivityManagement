import {Action} from '@ngrx/store'
import { User } from 'src/app/models/user/user';

export enum loggedIUserTypes {

    EDIT_LOGGEDUSER='[Logged User Edit]',
    RESET_LOGGEDUSER='[Logged User Reset]',
}

export class EditLoggedUser implements Action {
readonly type= loggedIUserTypes.EDIT_LOGGEDUSER;
constructor(public payload:User){}
}
export class ResetLoggedUser implements Action {
readonly type= loggedIUserTypes.RESET_LOGGEDUSER;
 

}
export type All= EditLoggedUser |ResetLoggedUser
