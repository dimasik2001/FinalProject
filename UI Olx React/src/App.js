import React from 'react';
import './App.css';
import {
  Switch,
  Route,
  Link,
} from "react-router-dom";
import {
  BrowserRouter as Router
} from "react-router-dom";
import LoginForm from './component/loginForm';
import RegisterForm from './component/registerForm';
import Users from './component/users';
import OlxNavBar from './component/olxNavBar';
import Ads from './component/ads'
import Profile from './component/profile';
import FilterControl from './component/filterControl';
import {Button} from 'react-bootstrap';
import AdViewer from './component/adViewer';
import {Favourites} from './component/favourites';

export default function App() {
  return (
    <Router>
      <div className="App">
        <OlxNavBar />
        <Switch>
          <Route path="/register" render={() => <div><RegisterForm /> <Link to="/login"> To login</Link> </div>} />
          <Route path="/login" render={() => <div><LoginForm /> <Link to="/register"> To registration</Link> </div>} />
          <Route path="/users" render={() => <Users />} />
          <Route exact path="/ads" render={() => <><FilterControl/><Ads/></>} />
          <Route path = "/ads/:id" component = {AdViewer}></Route>
          <Route path="/myAds" render={() => <><Ads displayMode="my"/></>} />
          <Route path="/profile" render={() => <Profile />} />
          <Route path="/favourites" component = {Favourites} /> 
        </Switch>
      </div>
    </Router>
  );
}
