import React from 'react';

const ShowDate = (props) => {
    function dateParser(dateString) {
        const date = new Date(dateString);
        return `${date.getDate()}-${date.getMonth()}-${date.getFullYear()} ${date.getHours()}:${date.getMinutes().toString().padStart(2, '0')}`;
    }
    return (
        <div className="grid">
            <div className="col-12"><p className="panel-text-descr">DATE</p></div>
            <div className="col-12"><p className="panel-text">{dateParser(props.date)}</p></div>
        </div>
    );
};

export default ShowDate;
