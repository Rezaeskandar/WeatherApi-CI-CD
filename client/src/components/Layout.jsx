import React from "react";
import styled from "styled-components"

const Title = styled.h1`
display : flex;
flex-direction: column;
justify-content: space-evenly;
align-items: center;
border: 10px solid green;
margin: 20px;
background: aqua;
`

function Layout(props) {

    return (
        <div>
            <Title>Welcome to our TeamProject of Static Data Weather API CI-CD</Title>
        </div>
    )
}

export default Layout;