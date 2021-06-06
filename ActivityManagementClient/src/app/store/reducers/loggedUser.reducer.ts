import { gender } from 'src/app/models/enums/gender';
import { User } from 'src/app/models/user/user';
import { loggedIUserTypes } from '../actions';
export * as loggedUserAction from '../actions';
import { EntityState, createEntityAdapter, EntityAdapter } from '@ngrx/entity'


export type loggedUserAction = loggedUserAction.All;

export interface UserLoggedState extends EntityState<User> {

}

export const adapter:EntityAdapter<User> = createEntityAdapter<User>();

export const initUserLoggedState= adapter.getInitialState();
export function loggedUserReducer(state = initUserLoggedState, action: loggedUserAction):UserLoggedState {
    switch (action.type) {
        case loggedIUserTypes.EDIT_LOGGEDUSER:

            return { ...state }

        default:
            return state;
    }
}