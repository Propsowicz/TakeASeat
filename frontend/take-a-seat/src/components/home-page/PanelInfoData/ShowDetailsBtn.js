/* eslint-disable */

import React from 'react';
import { Button } from 'primereact/button';
import { Link, useHistory } from 'react-router-dom';

const ShowDetailsBtn = (props) => {
    const navigate = useHistory().push
    const goToShowDetails = () => {
       navigate(`/${props.eventSlug}/${props.showId}/`)
    }

    return (
        <div className="grid">
            <div className="col-12"><p className="panel-text-descr" /></div>
            <div className="col-12"><p className="panel-text" />
                
                <Button label="Details" className="p-button-rounded" onClick={goToShowDetails}/>
                
            </div>
        </div>
    );
};

export default ShowDetailsBtn;
