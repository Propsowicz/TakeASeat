import React from 'react';

const ShowAvaibleTickets = props => (
    <div className="grid">
        <div className="col-12"><p className="panel-text-descr">TICKETS LEFT</p></div>
        {
                props.isReadyToSell
                ?
                    <div className="col-12"><p className="panel-text">{props.seatsLeft}</p></div>
                :
                    <div className="col-12"><p className="panel-text">Not available yet</p></div>
            }

    </div>
    );

export default ShowAvaibleTickets;
