/* eslint-disable */

import 'primereact/resources/themes/lara-dark-purple/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeflex/primeflex.css';

import React from 'react';
import { Switch, Route, BrowserRouter, HashRouter } from 'react-router-dom';
import './App.css';
import MainNavbar from './components/MainNavbar';
import HomePage from './pages/HomePage';
import MainFooter from './components/MainFooter';
import ShowDetails from './pages/ShowDetails';
import Login from './pages/UserService/Login';
import { UserContextProvider } from './context/UserContext';
import Payment from './pages/Payment';
import Shows from './pages/Shows'
import Events from './pages/Events'
import ShowsByTag from './pages/ShowsByTag';
import Register from './pages/UserService/Register';
import EditEventData from './pages/OrganizatorPages/EditEventData';
import OrganizatorPanel from './pages/OrganizatorPages/OrganizatorPanel';
import AdminPanel from './pages/AdminPages/AdminPanel';


function App() {
    return (
        <div>
            <HashRouter>
                <UserContextProvider>
                    <MainNavbar />
                    <Switch>    
                        <Route exact path="/"><HomePage /></Route>
                        <Route exact path="/tags/:tagName/" ><ShowsByTag /></Route>
                        <Route exact path="/organizator/main" ><OrganizatorPanel /></Route>
                        <Route exact path="/:slug/:showId/" ><ShowDetails /></Route>

                        <Route path="/shows/" ><Shows /></Route>
                        <Route exact path="/events/" ><Events /></Route>                     

                        <Route path="/edit/:eventId" ><EditEventData /></Route>

                        <Route path="/admin/" ><AdminPanel /></Route>                     

                        <Route path="/login/" ><Login /></Route>
                        <Route path="/register/" ><Register /></Route>

                        <Route path="/payment/"><Payment /></Route>
                    </Switch>
                    <MainFooter />
                </UserContextProvider>
            </HashRouter>            
        </div>
    );
}

export default App;
