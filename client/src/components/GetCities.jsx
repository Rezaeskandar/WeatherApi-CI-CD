import React, { useState, useEffect } from 'react';
import axios from 'axios';
import styled from 'styled-components';

const Background = styled.div`
  background: linear-gradient(135deg, #ff94c2, #ffdac1);
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

const CityList = styled.ul`
  list-style-type: none;
  padding: 0;
`;

const CityItem = styled.li`
  font-size: 18px;
  margin-bottom: 10px;
`;

function GetFavoriteCities() {
  const [cities, setCities] = useState([]);

  const fetchCities = async () => {
    try {
      const response = await axios.get('https://dev.kjeld.io:40200/favorite-cities');
      const data = response.data;
      setCities(data);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Background>
      <Container>
        <Title>Favorite Cities</Title>
        {cities.length > 0 ? (
          <CityList>
            {cities.map((city) => (
              <CityItem key={city.id}>{city.name}</CityItem>
            ))}
          </CityList>
        ) : (
          <p>No favorite cities found</p>
        )}
        <button onClick={fetchCities}>Get Favorite Cities</button>
      </Container>
    </Background>
  );
}

export default GetFavoriteCities;
