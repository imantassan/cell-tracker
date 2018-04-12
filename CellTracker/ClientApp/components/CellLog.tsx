import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as LogState from '../store/Log';

// At runtime, Redux will merge together...
type LogProps =
    LogState.LogState        // ... state we've requested from the Redux store
    & typeof LogState.actionCreators      // ... plus action creators we've requested
    & RouteComponentProps<{ dateFrom: string, dateTo: string }>; // ... plus incoming routing parameters


class CellLog extends React.Component<LogProps, {}> {

    componentWillMount() {
        const dateFrom = (this.props.match.params.dateFrom
            ? new Date(this.props.match.params.dateFrom)
            : this.getTodayDate()).toISOString();
        const dateTo = (this.props.match.params.dateTo ? new Date(this.props.match.params.dateTo) : this.getTodayDate())
            .toISOString();
        this.props.requestCallLog(dateFrom, dateTo);
    }

    componentWillReceiveProps(nextProps: LogProps) {
        const dateFrom = (nextProps.match.params.dateFrom
            ? new Date(nextProps.match.params.dateFrom)
            : this.getTodayDate()).toISOString();
        const dateTo = (nextProps.match.params.dateTo ? new Date(nextProps.match.params.dateTo) : this.getTodayDate())
            .toISOString();
        this.props.requestCallLog(dateFrom, dateTo);
    }

    private getTodayDate() {
        var today = new Date();
        today = new Date(today.getFullYear(), today.getMonth(), today.getDate());

        return today;
    }

    public render() {
        return <div>
                   <h1>Weather forecast</h1>
                   <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
               </div>;
    }

    private renderForecastsTable() {
        return <table className='table'>
                   <thead>
                   <tr>
                       <th>Date</th>
                       <th>Temp. (C)</th>
                       <th>Temp. (F)</th>
                       <th>Summary</th>
                   </tr>
                   </thead>
                   <tbody>
                   {this.props.calls.map((call, index) =>
                       <tr key={index}>
                        <td>{call.subscriberId}</td>
                        <td>{call.timestamp}</td>
                        <td>{call.duration}</td>
                    </tr>
                   )}
                   </tbody>
               </table>;
    }
}

export default connect(
    (state: ApplicationState) => state.log, // Selects which state properties are merged into the component's props
    LogState.actionCreators                 // Selects which action creators are merged into the component's props
)(CellLog) as typeof CellLog;