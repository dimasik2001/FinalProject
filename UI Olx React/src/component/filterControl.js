import React, { Component } from 'react'
import { Accordion, Form, Col, Row, Button } from 'react-bootstrap';
import { getUsers } from '../store/actions/usersActions';
import categories from '../store/categories/categories'
export default class filterControl extends Component {
  constructor(props) {
    super(props);
    this.onCategoryChange = this.onCategoryChange.bind(this)
    this.onDateFromChange = this.onDateFromChange.bind(this)
    this.onDateToChange = this.onDateToChange.bind(this)
    this.onPriceFromChange = this.onPriceFromChange.bind(this)
    this.onPriceToChange = this.onPriceToChange.bind(this)
    this.onSortItemChange = this.onSortItemChange.bind(this)
    this.onSortDirectionChange = this.onSortDirectionChange.bind(this)
    this.createQuerystring = this.createQuerystring.bind(this)
    this.onKeyWordsChange = this.onKeyWordsChange.bind(this)
  }
  componentDidMount() {
    const query = new URLSearchParams(window.location.search);
    this.setState({
      itemId: query.get("itemId"),
      dateFrom: query.get("dateFrom"),
      dateTo: query.get("dateTo"),
      priceFrom: query.get("priceFrom"),
      priceTo: query.get("priceTo"),
      sortItem: query.get("sortItem"),
      sortDirection: query.get("sortDirection"),
      keyWords : query.get("keyWords")
    }
    )

  }
  onKeyWordsChange(e){
    this.setState({ keyWords: e.target.value });
  }
  onDateFromChange(e) {
    debugger
    this.setState({ dateFrom: e.target.value });
  }
  onDateToChange(e) {
    debugger
    this.setState({ dateTo: e.target.value });
  }
  onPriceFromChange(e) {
    debugger
    this.setState({ priceFrom: e.target.value });
  }
  onPriceToChange(e) {
    debugger
    this.setState({ priceTo: e.target.value });
  }
  onCategoryChange(e) {
    this.setState({ itemId: e.target.value });
  }
  onSortItemChange(e) {
    this.setState({ sortItem: e.target.value });
  }
  onSortDirectionChange(e) {
    this.setState({ sortDirection: e.target.value })
  }
  createQuerystring() {
    debugger
    if (this.state === null) {
      return ''
    }
    const parameters = this.state;
    const query = new URLSearchParams();
    if (parameters?.itemId !== null && parameters?.itemId !== '-1') {
      query.set('itemId', parameters.itemId)
      query.set('filterItem', 'categoryId')
    }

    if (parameters?.dateFrom !== null && parameters?.dateFrom !== '') {
      query.set('dateFrom', parameters.dateFrom)
    }
    if (parameters?.dateTo !== null && parameters?.dateTo !== '') {
      query.set('dateTo', parameters.dateTo)
    }

    if (parameters?.priceFrom !== null && parameters?.priceFrom !== '') {
      query.set('priceFrom', parameters.priceFrom)
    }
    if (parameters?.priceTo !== null && parameters?.priceTo !== '') {
      query.set('priceTo', parameters.priceTo)
    }
    if (parameters?.sortItem !== null && parameters?.sortItem !== '-1') {
      query.set('sortItem', parameters.sortItem)
    }
    if (parameters?.sortDirection !== null && parameters?.sortDirection !== '-1') {
      query.set('sortDirection', parameters.sortDirection)
    }
    if (parameters?.keyWords !== null && parameters?.keyWords !== '') {
      query.set('keyWords', parameters.keyWords)
    }
    return `?${query.toString()}`;
  }
  render() {
    debugger
    const parameters = this.state;
    
    return (
      <>
        <Accordion defaultActiveKey="0">
          <Accordion.Item eventkey="1">
            <Accordion.Header>Parameters</Accordion.Header>
            <Accordion.Body>
            <Form.Group as={Col} className ="mt-3 mt-b">
                        <Form.Control type="text" value = {parameters?.keyWords} onChange={this.onKeyWordsChange} placeholder ="search.." />
             </Form.Group>
             <Form.Group as={Col} className ="mt-3 mt-b">
              <Form.Select className="mb-3" value={parameters?.itemId} onChange={this.onCategoryChange}>
                <option value={-1}>Category</option>
               {categories.map(c => <option value = {c.id}>{c.name}</option>)}
              </Form.Select>
              </Form.Group>
              <Accordion defaultActiveKey="0">
                <Accordion.Item eventkey="1">
                  <Accordion.Header>Published date</Accordion.Header>
                  <Accordion.Body>
                    <Row className="mb-3">
                      <Form.Group as={Col} >
                        <Form.Label>From:</Form.Label>
                        <Form.Control type="date" value={parameters?.dateFrom} onChange={this.onDateFromChange} />
                      </Form.Group>

                      <Form.Group as={Col} >
                        <Form.Label>To:</Form.Label>
                        <Form.Control type="date" value={parameters?.dateTo} onChange={this.onDateToChange} />
                      </Form.Group>
                    </Row>
                  </Accordion.Body>
                </Accordion.Item>
              </Accordion>

              <Accordion defaultActiveKey="0">
                <Accordion.Item eventkey="1">
                  <Accordion.Header>Price Interval</Accordion.Header>
                  <Accordion.Body>
                    <Row className="mb-3">
                      <Form.Group as={Col} >
                        <Form.Label>From:</Form.Label>
                        <Form.Control type="number" placeholder="1000" value={parameters?.priceFrom} onChange={this.onPriceFromChange} />
                      </Form.Group>

                      <Form.Group as={Col} >
                        <Form.Label>To:</Form.Label>
                        <Form.Control type="number" placeholder="9000" value={parameters?.priceTo} onChange={this.onPriceToChange} />
                      </Form.Group>
                    </Row>
                  </Accordion.Body>
                </Accordion.Item>
              </Accordion>

              <Accordion defaultActiveKey="0">
                <Accordion.Item eventkey="1">
                  <Accordion.Header>Order by</Accordion.Header>
                  <Accordion.Body>
                    <Row className="mb-3">
                      <Form.Group as={Col} >
                        <Form.Select className="mb-3" value={parameters?.sortItem} onChange={this.onSortItemChange}>
                          <option value="-1">Order item</option>
                          <option value="0">Price</option>
                          <option value="1">Published date</option>

                        </Form.Select>
                      </Form.Group>

                      <Form.Group as={Col} >
                        <Form.Select className="mb-3" value={parameters?.sortDirection} onChange={this.onSortDirectionChange}>
                          <option value="-1">Direction</option>
                          <option value="0">Ascending</option>
                          <option value="1">Descending</option>
                        </Form.Select>
                      </Form.Group>
                    </Row>
                  </Accordion.Body>
                </Accordion.Item>
              </Accordion>
              <Row>
                <Col></Col>
              <Col className>
              <Button variant="primary" className="mt-3" href={`/ads${this.createQuerystring()}`}>Apply</Button>
              </Col>
              <Col>
              <Button  variant="secondary" className="mt-3" href={`/ads`}>Reset</Button>
              </Col>
              <Col></Col>
              </Row>
              
            </Accordion.Body>
          </Accordion.Item>
        </Accordion>
      </>
    )
  }
}
