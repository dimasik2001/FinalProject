import { getFromFavourite, deleteFromFavourite } from '../store/actions/cookiesAction'
import React, { Component } from 'react'
import { Card, Button, Row, Col, Pagination } from 'react-bootstrap'
import host from '../store/actions/host/hostName';

export class Favourites extends Component {

    unlikeClick(id) {
        deleteFromFavourite(id);
        this.setState({ads:getFromFavourite()});
    }
    componentDidMount(){
        this.setState({ads:getFromFavourite()});
    }
    render() {
        debugger
       const ads = this.state?.ads;
        let image;
        if (ads == undefined || ads?.length == 0) {
            return (<>You haven't any favourites</>)
        }
        for (let i = 0; i < ads.length; i++) {
            if (ads[i].images != null && ads[i].images != undefined && ads[i].images.length>0) {
                ads[i].images[0] = host + ads[i].images[0];
            }
        }


        return (
            ads?.map(a =>
                <Row md={3} className="g-3">
                    <Col>
                        <Card>
                            <Card.Link href={`/ads/${a.id}`}>
                                <Card.Img variant="top" src={a.images[0]??`${host}unset/ad.png`} height="300" width="30" />
                            </Card.Link>
                            <Card.Body>
                                <Card.Title>{a.header}</Card.Title>
                                <Card.Text>
                                    <b>${a.price}</b><br />
                                </Card.Text>
                            </Card.Body>
                            <Button variant="danger" onClick={() => this.unlikeClick(a.id)}>Unlike this</Button>
                        </Card>
                    </Col>
                </Row>
            )
        )
    }
}
