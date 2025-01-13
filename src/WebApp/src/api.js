import axios from 'axios';

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL

export const getTodos = () => axios.get(`${API_BASE_URL}/api/todos`);
export const createTodo = (todo) => axios.post(`${API_BASE_URL}/api/todos`, todo);
export const markCompleted = (id) => axios.post(`${API_BASE_URL}/api/todos/${id}/completed`);
export const deleteTodo = (id) => axios.delete(`${API_BASE_URL}/api/todos/${id}`);