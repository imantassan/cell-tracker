import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as LogState from '../store/Log';
import { DateRange, defaultRanges } from 'react-date-range';
import * as moment from 'moment';

// At runtime, Redux will merge together...
type LogProps =
    LogState.LogState        // ... state we've requested from the Redux store
    & typeof LogState.actionCreators      // ... plus action creators we've requested
    & RouteComponentProps<{}>; // ... plus incoming routing parameters

class CellLog extends React.Component<LogProps, { dateFrom?: Date, dateTo?: Date }> {

    constructor(props: LogProps) {
        super(props);

        this.state = { dateFrom: undefined, dateTo: undefined };
    }

    private getTodayDate() {
        var today = new Date();
        today = new Date(today.getFullYear(), today.getMonth(), today.getDate());

        return today;
    }

    private handleDateFromChange(newValue: { startDate: moment.Moment, endDate: moment.Moment }) {
        const dateFrom = newValue.startDate.toDate();
        const dateTo = newValue.endDate.toDate();

        this.setState({ dateFrom: dateFrom, dateTo: dateTo });
        this.props.requestCallLog(dateFrom.toISOString(), dateTo.toISOString());
    }

    public render() {
        return <div>
            <h1>Unreal Cell Tracker</h1>
            <p>Note: Some Subscriber IDS (MSISDN) could match real personal identifying information, however they are completely randomly generated.</p>

            <div className="container-fluid table-bordered" style={{ padding: '15px', margin: '15px' }}>
                <p>Please select a date range</p>
                <DateRange
                    startDate={moment(this.state.dateFrom)}
                    endDate={moment(this.state.dateTo)}
                    linkedCalendars={true}
                    ranges={defaultRanges}
                    onChange={this.handleDateFromChange.bind(this)}
                    onInit={this.handleDateFromChange.bind(this)}
                />
            </div>
            <div className="container-fluid table-bordered" style={{ padding: '15px', margin: '15px' }}>
                <div className="row col-xs-12">
                    <div className="col-lg-6">
                        TOP Calls for the period:
                        <table className='table'>
                            <thead>
                                <tr>
                                    <th>Subscriber ID (MSISDN)</th>
                                    <th>Total duration</th>
                                    <th>Count</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.props.calls.map((call, index) =>
                                    <tr key={index}>
                                        <td>{call.subscriberId}</td>
                                        <td>{call.totalDuration}</td>
                                        <td>{call.totalCount}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>

                        Total number of calls for the period: {this.props.totalCallCount}
                    </div>

                    <div className="col-lg-6">
                        TOP SMS for the period:
                        <table className='table'>
                            <thead>
                                <tr>
                                    <th>Subscriber ID (MSISDN)</th>
                                    <th>Count</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.props.smses.map((call, index) =>
                                    <tr key={index}>
                                        <td>{call.subscriberId}</td>
                                        <td>{call.totalCount}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>

                        Total number of SMS for the period: {this.props.totalSmsCount}
                    </div>
                </div>
            </div>
        </div>;
    }
}

export default connect(
    (state: ApplicationState) => state.log, // Selects which state properties are merged into the component's props
    LogState.actionCreators                 // Selects which action creators are merged into the component's props
)(CellLog) as typeof CellLog;