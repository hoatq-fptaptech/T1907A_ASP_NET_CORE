import React from 'react'
import ProductForm from './ProductForm'
export default class Products extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            products: [],
            showForm: false
        }
        this.reload = this.reload.bind(this);
    }
    componentDidMount() {
        fetch("api/products").then(rs => rs.json()).then(rs => {
            // console.log(rs);
            this.setState({ products: rs });
        })
    }
    reload() {
        fetch("api/products").then(rs => rs.json()).then(rs => {
            // console.log(rs);
            this.setState({ products: rs });
        })
    }
    render() {
        const products = this.state.products;
        const showForm = this.state.showForm;
        return (
            showForm ? <ProductForm onReload={this.reload}  onBack={() => { this.setState({ showForm: false }); }}/>:
            <div>
                <h1>Product Listing</h1>
                <button className="btn btn-primary" onClick={() => this.setState({ showForm: true })}>New Product</button>
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Image</th>
                            <th>Price</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                                products.map((e, i) => {
                                    return <tr key={i}>
                                        <td>{e.id}</td>
                                        <td>{e.name}</td>
                                        <td>{e.image}</td>
                                        <td>{e.price}</td>
                                        <td></td>
                                    </tr>
                            })
                        }
                    </tbody>
                </table>
            </div>
            )     
    }
}