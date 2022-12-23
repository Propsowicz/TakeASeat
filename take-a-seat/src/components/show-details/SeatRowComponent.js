/* eslint-disable */

import React, {useState} from 'react'
import SeatComponent from './SeatComponent'

export const SeatRowComponent = (props) => {
    console.log('')
    const [rowData, setRowData]= useState(props.row)
    console.log(rowData)

  return (    
    <div className="card">
        <div className="card-container overflow-hidden">
            <div className="flex gap-2 seat-rows">                
            {rowData.map((seat) => (
                <SeatComponent color={seat.seatColor} row={seat.row} price={seat.price} position={seat.position}/>
            ))}
            </div>
            
                                
        </div>
    </div>  
  )
}
