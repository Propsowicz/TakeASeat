/* eslint-disable */

import React from 'react';
import { dateSerializer } from '../../../utils/dateSerializer';

const ShowDate = (props) => {
    
    return (
        <div className="grid">
            <div className="col-12"><p className="panel-text-descr">DATE</p></div>
            <div className="col-12"><p className="panel-text">{dateSerializer(props.date)}</p></div>
        </div>
    );
};

export default ShowDate;
