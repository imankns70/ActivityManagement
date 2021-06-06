import * as fromRoot from 'src/app/store'
import {createSelector} from '@ngrx/store';
import {RouterStateUrl} from 'src/app/store/_model/routerStateUrl';

export const getUserIdRouterParams= (state:any)=> state.UserId;

export const getRouterUserId= createSelector(

    fromRoot.getRouterParamsState,
    getUserIdRouterParams)
 