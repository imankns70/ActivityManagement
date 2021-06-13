import { Injectable } from "@angular/core";
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from "rxjs";
import { catchError, map, switchMap } from "rxjs/operators";
import { UserService } from "src/app/components/panel/services/user.service";
import { ApiResult } from "src/app/models/apiresult";

import * as loggedUserActions from '../actions/loggedUser.action'
@Injectable()

export class LoggedUserEffects {

    constructor(private action$: Actions, private userService: UserService) {

    }

    @Effect()
    LoadLoggedUser$ = this.action$.pipe(ofType(loggedUserActions.loggedIUserTypes.LOADLOGGEDUSER))
        .pipe(
            switchMap(() => {
                debugger;
                return this.userService.GetUserLoggedIn().pipe(
                    map(resp => new loggedUserActions.LoadLoggedUserSuccess(resp.data)),
                    catchError(err => of(new loggedUserActions.LoadLoggedUserFail(err)))
                );
            })
        );

}