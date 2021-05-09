import { Action } from "@ngrx/store"


export const EDIT_TITLE = '[TitleCounter] Edit'
export const DECREASECounter = '[Counter] Decrease Counter'
export const INCREASECounter = '[Counter] Increase Counter'
export const RESETCounter = '[Counter] Reset Counter'

export class EditTitle implements Action {
    readonly type = EDIT_TITLE;

    constructor(public payload: string) {

    }

}

export class DecreaseCounter implements Action {
    readonly type = DECREASECounter;
    constructor() {

    }

}

export class IncreaseCounter implements Action {
    readonly type = INCREASECounter;
    constructor() {

    }

}

export class ResetCounter implements Action {
    readonly type = RESETCounter;
    constructor() {

    }

}
export type All = EditTitle | IncreaseCounter | DecreaseCounter | ResetCounter;