/* eslint-disable */

import React, { useEffect, useState } from 'react';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import SeatComponent from './SeatComponent';
import { SeatRowComponent } from './SeatRowComponent';


const CreateSeats = (props) => {
    const seatsColorDropdown = [
        {label: "Red", value: 'red'},
        {label: "Blue", value: 'blue'},
        {label: "Yellow", value: 'yellow'},
        {label: "Green", value: 'green'},
        {label: "Orange", value: 'orange'}
    ]
    const [seatColorTemp, setSeatsColorTemp] = useState()
    const [newSeats, setNewSeats] = useState([])
    const listOfRowsDefault = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K']
    const [listOfRows, setListOfRows] = useState(listOfRowsDefault)
    const [rowsCounter, setRowsCounter] = useState(0)
    

    const createNewRow = (e) => {
        e.preventDefault()
        let tempListOfRows = listOfRows
        let tempTable = []
        let numOfSeats = e.target.numOfSeats.value
        let rowPrice = e.target.rowPrice.value
        let rowColor = e.target.rowColor.value
        
        if (rowsCounter < listOfRowsDefault.length){
            for(let i = 0; i < numOfSeats; i++){
                tempTable.push({
                    "showId": props.showId,
                    "row": tempListOfRows[0],
                    "position": i + 1,
                    "price": rowPrice,
                    "seatColor": rowColor
                })
            }
            setNewSeats(newSeats => [...newSeats, tempTable])
        }else{
            console.log(`You can't create more rows than ${listOfRowsDefault.length}.`)
        }  

        tempListOfRows.splice(0, 1)
        setListOfRows(tempListOfRows)        
        setRowsCounter(rowsCounter + 1)
    }


    useEffect(() => {
        console.log(newSeats)
    },[newSeats])

    return (
        <div>
            <div className="card">
                <div className="card-container overflow-hidden">
                    <div>
                        {newSeats.map((row) => (
                            <SeatRowComponent row={row}/>
                        ))}
                    </div>                    
                        <div className="flex gap-2"> 
                            <form onSubmit={createNewRow} className="flex gap-2 add-seat-form">     
                                <div className="flex align-items-center justify-content-center"><InputNumber name='numOfSeats' min={1} max={20} placeholder="Number of seats in a row" tooltip='Between 1 and 40 seats.' required/></div>
                                <div className="flex align-items-center justify-content-center"><InputNumber name='rowPrice' min={0} max={2000} placeholder="Tickets price in a row" tooltip='Between 0 and 2000$' currency='USD' required/></div>
                                <div className="flex align-items-center justify-content-center"><Dropdown name='rowColor' value={seatColorTemp} options={seatsColorDropdown} onChange={(e) => setSeatsColorTemp(e.value)} placeholder="Select seats color" required/></div>
                                <div className="flex align-items-center justify-content-center"><Button label="Add new row" /></div>
                            </form>    
                        </div>                    
                </div>
            </div>
        </div>
    );
};

export default CreateSeats;
