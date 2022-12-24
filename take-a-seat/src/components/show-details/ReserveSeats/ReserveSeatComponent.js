/* eslint-disable */

import React, {useState, useEffect} from 'react'
import { Tooltip } from 'primereact/tooltip';
import { Button } from 'primereact/button';

const ReserveSeatComponent = (props) => {
    const [rowData, setRowData]= useState(props.row)
    const [reservedSeatsList, setReservedSeatsList] = useState([])
    
    const reserveSeats = async (e) => {     
        if(e.target.offsetParent.className === 'p-button p-component seat-component'){
            e.target.offsetParent.style.background = "black"
        }else if (e.target.offsetParent.className === ''){
            e.target.style.background = "black"
        }                
        e.target.style.color = "white"

        let reservedTempSeatData = {
            "showId": parseInt(props.showId),
            "row": e.target.innerText[0],
            "position": e.target.innerText.slice(1),
            "price": rowData[0].price,            
        }
        console.log(typeof(reservedTempSeatData))  
        console.log(reservedTempSeatData)  

        setReservedSeatsList(reservedSeatsList => [...reservedSeatsList, reservedTempSeatData])
        console.log(reservedSeatsList)  

    }
    

    useEffect(() => {
        console.log(reservedSeatsList)  
    }, [reservedSeatsList])

  return (
    <div className="card">
        <div className="card-container overflow-hidden">
            <div className="flex gap-2 seat-rows">                           
            {rowData.map((seat) => (
                <div>
                    <Button type="button" label={seat.row + seat.position} className='seat-component' 
                    style={{'background': `${seat.seatColor}`}} onClick={reserveSeats}/>
                
                    <Tooltip target=".seat-component" autoHide={false}>
                        <div className='seat-tooltip'>
                            <p>Price: {seat.price}$</p>              
                        </div>
                    </Tooltip>
                </div>                
            ))}
            </div>           
                                
        </div>
    </div>
  )
}

export default ReserveSeatComponent