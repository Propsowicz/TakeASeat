/* eslint-disable */

import React, {useState} from 'react';
import { Tooltip } from 'primereact/tooltip';
import { Button } from 'primereact/button';

const SeatComponent = (props) => {
    
    const [row, setRow] = useState(props.row)
    const [seatColor, setseatColor] = useState(props.color)
    

    return (
        <div>
            <Button type="button" label={props.row + props.position} className='seat-component' style={{'background': `${props.color}`}} />
            
            <Tooltip target=".seat-component" autoHide={false}>
                <div className='seat-tooltip'>
                    <p>Price: {props.price}$</p>              
                </div>
            </Tooltip>


            
        </div>
    );
};



export default SeatComponent;
