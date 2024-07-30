import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

function ErrorModal(props) {
  return (
    <Modal show={props.show} centered>
    <Modal.Header closeButton>
      <Modal.Title>{props.title}</Modal.Title>
    </Modal.Header>
    <Modal.Body>{props.body}</Modal.Body>
    <Modal.Footer>
      <Button variant="secondary"  onClick={props.handleClose}>
        Close
      </Button>
    </Modal.Footer>
  </Modal>
  );
}

export default ErrorModal;