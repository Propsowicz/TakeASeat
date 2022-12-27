/* eslint-disable */
import React from 'react';
import ShowAvaibleTickets from './PanelInfoData/ShowAvaibleTickets';
import ShowDate from './PanelInfoData/ShowDate';
import ShowDescription from './PanelInfoData/ShowDescription';
import ShowDetailsBtn from './PanelInfoData/ShowDetailsBtn';
import ShowPlace from './PanelInfoData/ShowPlace';


const PanelMainInfo = (props) => {
    return (
        <div className='grid'>  
            <div className="col-12 md:col-6 lg:col-3"><p className="panel-text"><ShowDescription showDescr={props.showDescr}/></p></div>
            <div className="col-12 md:col-6 lg:col-2"><p className="panel-text"><ShowDate date={props.date}/></p></div>
            <div className="col-12 md:col-6 lg:col-3"><p className="panel-text"><ShowPlace place={props.place}/></p></div>
            <div className="col-12 md:col-6 lg:col-2"><p className="panel-text"><ShowAvaibleTickets /></p></div>
            <div className="col-12 md:col-6 lg:col-2"><p className="panel-text"><ShowDetailsBtn eventId={props.eventId} showId={props.showId} eventSlug={props.eventSlug}/></p></div>
        </div>
    );
};

export default PanelMainInfo;
