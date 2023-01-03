/* eslint-disable */

import React, { useEffect, useState } from 'react';

const ReservedSeats = (props) => {
    
    return (
        <div>
            <span>Reserved seats: </span>
            {props.list.map(seat => (
                <button type="button" className={'seat-component font-medium custom-gap-2'}>
                    {seat.row + seat.position}
                </button>
            ))}
        </div>
    );
};

export default ReservedSeats;
