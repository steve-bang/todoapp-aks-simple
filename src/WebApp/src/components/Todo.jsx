import React, { useEffect, useState } from 'react';
import { getTodos, createTodo, deleteTodo, markCompleted } from '../api';

function TodoPage() {
  const [todos, setTodos] = useState([]);
  const [todoName, setTodoName] = useState('');

  useEffect(() => {
    fetchTodos();
  }, []);

  const fetchTodos = async () => {
    try {
      const response = await getTodos();
      setTodos(response.data);
    } catch (error) {
      console.error('Error fetching todos:', error);
    }
  };

  const handleAddTodo = async () => {
    if (!todoName.trim()) return;
    try {
      await createTodo({ name: todoName });
      setTodoName('');
      fetchTodos();
    } catch (error) {
      console.error('Error creating todo:', error);
    }
  };

  const handleDeleteTodo = async (id) => {
    try {
      await deleteTodo(id);
      fetchTodos();
    } catch (error) {
      console.error('Error deleting todo:', error);
    }
  };

  const handleOnChecked = async (id) => {
    try {
      await markCompleted(id);
      fetchTodos();
    } catch (error) {
      console.error('Error mark completing todo:', error);
    }
  }

  return (
    <div style={{ padding: '20px' }}>
      <h1>Todo List</h1>
      <input
        type="text"
        placeholder="Enter todo name"
        value={todoName}
        onChange={(e) => setTodoName(e.target.value)}
      />
      <button onClick={handleAddTodo}>Add Todo</button>
      <ul>
        {todos.map((todo) => (
          <li key={todo.id}>
            {todo.name}
            <input type='checkbox' checked={todo.isCompleted} onChange={() => handleOnChecked(todo.id)}/>
            <button onClick={() => handleDeleteTodo(todo.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TodoPage;
