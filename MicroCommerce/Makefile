pcimage:
	docker image rm product-catalog
	docker build -t product-catalog -f ./Docker/ProductCatalog/Dockerfile .

pccont: 
	docker run -d --name ProductCatalog -p 3000:80 product-catalog

pcstop:
	docker stop ProductCatalog
	docker rm ProductCatalog
