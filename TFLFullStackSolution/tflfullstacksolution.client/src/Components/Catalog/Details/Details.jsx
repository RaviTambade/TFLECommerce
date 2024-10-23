 
import './Details.css'; // Optional: For styling

function Details({ item }) {

    if (!item) {
        return <p>Select an item to view details.</p>;
    }

    return (
        <div >
            <h2>{item.title}</h2>
            <img src={item.imageurl} width="100" height="100" />
            <p><strong>Description:</strong> {item.details}</p>
            <p><strong>Unit Price:</strong> {item.unitprice}</p>
            <p><strong>Stock Available:</strong> {item.quantity}</p>
            <p><strong>Likes:</strong> {item.likes}</p>
        </div>
    );
}
export default Details;
