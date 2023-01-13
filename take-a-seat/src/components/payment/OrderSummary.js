import React from 'react';
import { Divider } from 'primereact/divider';
import { Button } from 'primereact/button';


const OrderSummary = (props) => {
    console.log('elo');
    return (
        <div className="order-summary-cart grid">
            <div className="col-12 md:col-12 lg:col-12"><p className="panel-text">Customer data:</p></div>
            <div className="col-12 md:col-12 lg:col-3 grid">
                <p className="panel-text-descr col-12">USERNAME</p>
                <p className="panel-text col-12">{props.userName}</p>
            </div>
            <div className="col-12 md:col-12 lg:col-3 grid">
                <p className="panel-text-descr col-12">FIRST NAME</p>
                <p className="panel-text col-12">{props.firstName}</p>
            </div>
            <div className="col-12 md:col-12 lg:col-3 grid">
                <p className="panel-text-descr col-12">LAST NAME</p>
                <p className="panel-text col-12">{props.lastName}</p>
            </div>
            <div className="col-12 md:col-12 lg:col-3 grid">
                <p className="panel-text-descr col-12">E-MAIL</p>
                <p className="panel-text col-12">{props.email}</p>
            </div>
            <Divider />

            <div className="col-12 md:col-12 lg:col-9 grid">
                <p className="panel-text-descr col-12">SUMMARY</p>
                <p className="panel-text col-12">{props.data.amount}$</p>
            </div>
            <div className="col-12 md:col-12 lg:col-3 grid">
                <p className="panel-text-descr col-12" />

                <form action="https://ssl.dotpay.pl/test_payment/" method="post" target="_parent">
                    <input type="hidden" name="amount" value={props.data.amount} />
                    <input type="hidden" name="type" value={props.data.type} />
                    <input type="hidden" name="currency" value={props.data.currency} />
                    <input type="hidden" name="id" value={props.data.id} />
                    <input type="hidden" name="description" value={props.data.description} />
                    <input type="hidden" name="url" value={props.data.url} />
                    <input type="hidden" name="urlc" value={props.data.urlc} />
                    <input type="hidden" name="chk" value={props.data.chk} />
                    <Button label="Go to Payment" />
                </form>


            </div>

        </div>
    );
};

export default OrderSummary;
