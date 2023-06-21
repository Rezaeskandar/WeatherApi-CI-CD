import React, { useState } from 'react';
import axios from 'axios';
import styled from 'styled-components';

const Background = styled.div`
  background: linear-gradient(135deg, #fddb92, #d1fdff);
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
`;

const Container = styled.div`
  background-color: rgba(255, 255, 255, 0.9);
  padding: 40px;
  border-radius: 12px;
  text-align: center;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
`;

const Title = styled.h1`
  font-size: 24px;
  margin-bottom: 20px;
`;

const Input = styled.input`
  font-size: 16px;
  padding: 10px;
  border-radius: 8px;
  border: none;
  margin-bottom: 20px;
  width: 300px;
`;

const Button = styled.button`
  font-size: 16px;
  padding: 10px 20px;
  border-radius: 8px;
  background-color: #fff;
  color: #0072ff;
  border: 1px solid #0072ff;
  cursor: pointer;
  outline: none;
`;

const SuccessMessage = styled.p`
  color: green;
`;

const ErrorMessage = styled.p`
  color: red;
`;

function SaveFavoriteCities() {
  const [cityName, setCityName] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleInputChange = (e) => {
    setCityName(e.target.value);
  };

  const handleSaveCity = async () => {
    if (cityName.trim() === '') {
      setErrorMessage('Please enter a city name');
      return;
    }

    if (cityName.trim().toLowerCase() === successMessage.toLowerCase()) {
      setErrorMessage('This city is already saved');
      return;
    }

    try {
      const response = await axios.post('https://dev.kjeld.io:40200/favorite-city', { name: cityName });
      setSuccessMessage(cityName);
      setErrorMessage('');
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Background>
      <Container>
        <Title>Save Your Favorite Cities</Title>
        <Input type="text" placeholder="Enter city name" value={cityName} onChange={handleInputChange} />
        <Button onClick={handleSaveCity}>Save City</Button>
        {errorMessage && <ErrorMessage>{errorMessage}</ErrorMessage>}
        {successMessage && <SuccessMessage>{successMessage} saved successfully!</SuccessMessage>}
      </Container>
    </Background>
  );
}

export default SaveFavoriteCities;
