import { ActionReducerMap } from "@ngrx/store";
import { loggedUserReducer } from "./loggedUser.reducer";
import * as fromRouter from '@ngrx/router-store';
import { RouterStateUrl } from "../_model/routerStateUrl";
import { User } from "src/app/models/user/user";


 
export interface State {
    router: fromRouter.RouterReducerState<RouterStateUrl>;
    loggedUser: User;
}

export const reducers: ActionReducerMap<State> = {
    router: fromRouter.routerReducer,
    loggedUser: loggedUserReducer
}
 