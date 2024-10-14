import './App.css';
import  { useState } from 'react';
import List from '../../Components/Catalog/List/List.jsx';
import Details from '../../Components/Catalog/Details/Details';

function App() {
    // Sample data

    const [items] = useState([
        { title: 'Marigold', details: 'A festival Flower', unitprice: 2, quantity: 236, likes: 5600, imageurl:"/images/marigold.jpg" },
        { title: 'Gerbera', details: 'A Wedding Flower', unitprice: 8, quantity: 67, likes: 8000, imageurl: "/images/gerbera.jpg" },
        { title: 'Rose', details: 'A  Valentine Flower', unitprice: 12, quantity: 4348, likes: 3400, imageurl: "/images/rose.jpg" },
        { title: 'Lotus', details: 'A Worship Flower', unitprice: 22, quantity: 7654, likes: 6523, imageurl: "/images/lotus.jpg" }
    ]);

    //While defining state for component
    //set state name as first parameter
    //set nameof function which will change the state: setSelectedItem
    const [selectedItem, setSelectedItem] = useState(null);

    const handleItemClick = (item) => {
        setSelectedItem(item);
    };


    //Rendering logic

    return (
        <div>
            <h1>Todays Fresh Flowers</h1>

            <div class="container mt-5">
                <div class="row">
                    <div class="col-sm-4">
                        <h3>Select Flower</h3>
                        <List items={items} onItemClick={handleItemClick} />
                     </div>
                    <div class="col-sm-8">
                        <Details item={selectedItem} />
                    </div>
                </div>
            </div>
        </div>
    );
}
export default App;