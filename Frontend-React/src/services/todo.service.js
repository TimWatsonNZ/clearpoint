import axios from 'axios';
import Maybe from '../maybe';

const endpoint = "https://localhost:5001/api/todoitems";

async function getItems() {
    try {
        var items = await axios.get(endpoint);

        return items.data;
    } catch (error) {
        console.error(error);
    }
}

async function createItem(item) {
    try {
        var response = await axios.post(
            endpoint,
            item
        );
        return new Maybe(response.data);
    } catch (error) {
        return new Maybe(null, error);
    }
}

async function updateItem(item) {
    try {
        var response = await axios.put(`${endpoint}/${item.id}`, item);
        return response.data;
    } catch (error) {
        console.error(error);
    }
}

const TodoService = {
    getItems,
    createItem,
    updateItem
}

export default TodoService;