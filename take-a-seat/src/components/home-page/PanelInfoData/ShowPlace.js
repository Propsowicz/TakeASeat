import React from 'react';

const ShowPlace = (props) => {
    console.log('hi');
    return (
        <div className="grid">
            <div className="col-12"><p className="panel-text-descr">Place</p></div>
            <div className="col-12"><p className="panel-text">{props.place}</p></div>
        </div>
    );
};

export default ShowPlace;
