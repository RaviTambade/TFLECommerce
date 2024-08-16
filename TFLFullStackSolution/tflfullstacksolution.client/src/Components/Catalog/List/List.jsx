function List({ items, onItemClick }) {
    return (
        <div>
            <ul>
                {
                  items.map((item, index) => (<li key={index} onClick={() => onItemClick(item)}>{item.title}</li>))
                }
            </ul>
        </div>
    );
}

export default List;