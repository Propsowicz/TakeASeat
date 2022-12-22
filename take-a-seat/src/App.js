/* eslint-disable */

import 'primereact/resources/themes/arya-green/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeflex/primeflex.css';

import React from 'react';
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import './App.css';
import MainNavbar from './components/MainNavbar';
import HomePage from './pages/HomePage';
import MainFooter from './components/MainFooter';
import ShowDetails from './pages/ShowDetails';


function App() {
    return (
        <div>
            <BrowserRouter>
                <MainNavbar />
                <Routes>    
                    <Route path="/" element={<HomePage />}/>
                    <Route path="/:eventId/:showId/" element={<ShowDetails />}/>
                </Routes>
                <MainFooter />
            </BrowserRouter>            
        </div>
    );
}

export default App;
