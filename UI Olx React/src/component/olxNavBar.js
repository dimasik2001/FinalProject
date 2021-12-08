import React, { Component, Profiler } from 'react'
import { Navbar, Container, Nav } from 'react-bootstrap'
import host from '../store/actions/host/hostName'
export default class olxNavBar extends Component {
    render() {
        const retrievedStoreStr = localStorage.getItem('userData') 
        const userData = JSON.parse(retrievedStoreStr) 
        debugger
        let image
        let profileRef = "/profile";
        let profileAdsRef = `/myAds`
        let userName
        if(userData.userName === '')
        {
            userName = "Guest";
        }
        else{
          userName =userData.userName;
        }
        if(!userData.isLogin)
        {
            profileRef = "/login"
            profileAdsRef = "/login"
        }
        if(userData.imageUrl === null||userData.imageUrl === '')
        {
            image = `${host}unset/profile.png`
        }
        else
        {
          image = host + userData.imageUrl
        }
        return (
            <>
  <Navbar bg="dark" variant="dark" sticky="top">
    <Container>
      <Navbar.Brand href= {profileRef}>
        <img
          alt=""
          src={image}
          width="30"
          height="30"
          className="d-inline-block align-top rounded-circle"
        />{' '}
      Welcome, {userName}!
      </Navbar.Brand>
    <Navbar.Toggle aria-controls="basic-navbar-nav" />
    <Navbar.Collapse id="basic-navbar-nav">
      <Nav className="me-auto">
        <Nav.Link href={profileAdsRef}>My Ads</Nav.Link>
        <Nav.Link href="/ads">Home</Nav.Link>
      </Nav>
      <Nav className="me-auto d-flex flex-row-reverse">
        <Nav.Link href="/favourites">My Favourites</Nav.Link>
        <Nav.Link href="/login">Login</Nav.Link>
      </Nav>
    </Navbar.Collapse>
  </Container>
  </Navbar>
</>
        )
    }
}
