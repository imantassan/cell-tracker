import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface LogState {
    isLoading: boolean;
    dateFrom?: string;
    dateTo?: string;
    calls: CallLogRecord[];
    smses: SmsLogRecord[];
    totalCallCount: number;
    totalSmsCount: number;
}   
export interface LogRecord {
    subscriberId: string;
    totalCount: string;
}

export interface CallLogRecord extends LogRecord {
    totalDuration: string;
}

export interface SmsLogRecord extends LogRecord {
}

export interface LogResponse {
    success: boolean;
    totalCount: number;
}

export interface CallLogResponse extends LogResponse {
    topRecords: CallLogRecord[];
}

export interface SmsLogResponse extends LogResponse {
    topRecords: SmsLogRecord[];
}

interface RequestCallLogAction {
    type: 'CALLLOG_REQUEST';
    dateFrom?: string;
    dateTo?: string;
}

interface RetrieveCallLogAction {
    type: 'CALLLOG_RETRIEVE';
    dateFrom?: string;
    dateTo?: string;
    log: CallLogRecord[];
    count: number;
}

interface RequestSmsLogAction {
    type: 'SMSLOG_REQUEST';
    dateFrom?: string;
    dateTo?: string;
}

interface RetrieveSmsLogAction {
    type: 'SMSLOG_RETRIEVE',
    dateFrom?: string;
    dateTo?: string;
    log: SmsLogRecord[];
    count: number;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCallLogAction | RetrieveCallLogAction | RequestSmsLogAction | RetrieveSmsLogAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {

    requestCallLog: (dateFrom: string, dateTo: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        if (dateFrom !== getState().log.dateFrom || dateTo !== getState().log.dateTo) {
            let fetchCallsTask = fetch(`api/CallLog?dateFrom=${ dateFrom }&dateTo=${dateTo}`)
                .then(response => response.json() as Promise<CallLogResponse>)
                .then(data => {
                    dispatch({ type: 'CALLLOG_RETRIEVE', dateFrom: dateFrom, dateTo: dateTo, log: data.topRecords, count: data.totalCount });
                });

            let fetchSmsesTask = fetch(`api/SmsLog?dateFrom=${dateFrom}&dateTo=${dateTo}`)
                .then(response => response.json() as Promise<SmsLogResponse>)
                .then(data => {
                    dispatch({ type: 'SMSLOG_RETRIEVE', dateFrom: dateFrom, dateTo: dateTo, log: data.topRecords, count: data.totalCount });
                });

            addTask(fetchCallsTask); // Ensure server-side prerendering waits for this to complete
            addTask(fetchSmsesTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: 'CALLLOG_REQUEST', dateFrom: dateFrom, dateTo: dateTo });
            dispatch({ type: 'SMSLOG_REQUEST', dateFrom: dateFrom, dateTo: dateTo });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: LogState = { calls: [], smses: [], totalSmsCount: 0, totalCallCount: 0, isLoading: false };

export const reducer: Reducer<LogState> = (state: LogState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'CALLLOG_REQUEST':
        case 'SMSLOG_REQUEST':
            return {
                dateFrom: action.dateFrom,
                dateTo: action.dateTo,
                calls: state.calls,
                smses: state.smses,
                totalCallCount: state.totalCallCount,
                totalSmsCount: state.totalSmsCount,
                isLoading: true
            };
        case 'CALLLOG_RETRIEVE':
            if (action.dateFrom === state.dateFrom && action.dateTo === state.dateTo) {
                return {
                    dateFrom: action.dateFrom,
                    dateTo: action.dateTo,
                    calls: action.log,
                    smses: state.smses,
                    totalCallCount: action.count,
                    totalSmsCount: state.totalSmsCount,
                    isLoading: false
                };
            }
            break;
        case 'SMSLOG_RETRIEVE':
            if (action.dateFrom === state.dateFrom && action.dateTo === state.dateTo) {
                return {
                    dateFrom: action.dateFrom,
                    dateTo: action.dateTo,
                    calls: state.calls,
                    smses: action.log,
                    totalCallCount: state.totalCallCount,
                    totalSmsCount: action.count,
                    isLoading: false
                };
            }
            break;
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
