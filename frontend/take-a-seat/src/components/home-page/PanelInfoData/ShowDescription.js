import React from 'react';

const ShowDescription = props => (
    <div className="grid">
        <div className="col-12"><p className="panel-text-descr">SHOW DESCRIPTION</p></div>
        <div className="col-12"><p className="panel-text">{props.showDescr}</p></div>
    </div>
    );

export default ShowDescription;
