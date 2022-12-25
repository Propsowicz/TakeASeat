/* eslint-disable */

import React, { useEffect, useState } from 'react';

const ReservedSeats = (props) => {
    const [reservedSeatsList, setReservedSeatsList] = useState(props.list);
    
    return (
        <div>
            {props.list.map(seat => (
                <button type="button" className={'seat-component text-500 '}>
                    {seat.row + seat.position}
                </button>
            ))}

        </div>
    );
};

export default ReservedSeats;
