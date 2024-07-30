import './App.css'
import { Image, Alert, Container, Row, Col } from 'react-bootstrap'
import React, { useState, useEffect } from 'react'
import AddTodoItem from './components/AddTodoItem';
import TodoItemList from './components/TodoItemList';
import TodoService from './services/todo.service';
import ErrorModal from './components/ErrorModal';

const App = () => {
  const [items, setItems] = useState([]);
  const [errorBody, setErrorBody] = useState("");
  const [errorTitle, setErrorTitle] = useState("");
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    getItems();
  }, [])

  async function getItems() {
    var items = await TodoService.getItems();
    setItems(items);
  }

  const handleAdd = async (description) => {
    var item = await TodoService.createItem({
      description,
      completed: false
    });

    if (item.value) {
      setItems([...items, item.value]);
    } else {
      openErrorModal(item.error)
    }  
  }

  const openErrorModal = (error) => {
    setShowModal(true);
    setErrorTitle(error.title);
    setErrorBody(error.message);
  }

  const handleMarkAsComplete = async (item) => {
    item.isCompleted = true;
    await TodoService.updateItem(item);

    setItems(items.filter(i => i.id !== item.id));
    return item;
  }

  return (
    <div className="App">
      <ErrorModal show={showModal} title={errorTitle} body={errorBody} handleClose={() => setShowModal(false)} />
      <Container>
        <Row>
          <Col>
            <Image src="clearPointLogo.png" fluid rounded />
          </Col>
        </Row>
        <Row>
          <Col>
            <Alert variant="success">
              <Alert.Heading>Todo List App</Alert.Heading>
              Welcome to the ClearPoint frontend technical test. We like to keep things simple, yet clean so your
              task(s) are as follows:
              <br />
              <br />
              <ol className="list-left">
                <li>Add the ability to add (POST) a Todo Item by calling the backend API</li>
                <li>
                  Display (GET) all the current Todo Items in the below grid and display them in any order you wish
                </li>
                <li>
                  Bonus points for completing the 'Mark as completed' button code for allowing users to update and mark
                  a specific Todo Item as completed and for displaying any relevant validation errors/ messages from the
                  API in the UI
                </li>
                <li>Feel free to add unit tests and refactor the component(s) as best you see fit</li>
              </ol>
            </Alert>
          </Col>
        </Row>
        <Row>
          <Col><AddTodoItem handleAdd={handleAdd} /></Col>
        </Row>
        <br />
        <Row>
          <Col><TodoItemList items={items} getItems={getItems} handleMarkAsComplete={handleMarkAsComplete}/></Col>
        </Row>
      </Container>
      <footer className="page-footer font-small teal pt-4">
        <div className="footer-copyright text-center py-3">
          © 2021 Copyright:
          <a href="https://clearpoint.digital" target="_blank" rel="noreferrer">
            clearpoint.digital
          </a>
        </div>
      </footer>
    </div>
  )
}

export default App
