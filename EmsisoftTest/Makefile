help: ## help dialog
	@IFS=$$'\n' ; \
	help_lines=(`fgrep -h "##" $(MAKEFILE_LIST) | fgrep -v fgrep | sed -e 's/\\$$//'`); \
	for help_line in $${help_lines[@]}; do \
		IFS=$$'#' ; \
		help_split=($$help_line) ; \
		help_command=`echo $${help_split[0]} | sed -e 's/^ *//' -e 's/ *$$//'` ; \
		help_info=`echo $${help_split[2]} | sed -e 's/^ *//' -e 's/ *$$//'` ; \
		printf "%-30s %s\n" $$help_command $$help_info ; \
	done

api: ## build docker container of api
	docker build -t emsisoft-test-api:1.0.0 -f ./EmsisoftTest.Api/Dockerfile .
	docker tag emsisoft-test-api:1.0.0 emsisoft-test-api:latest

processor: ## build docker container of processor
	docker build -t emsisoft-test-processor:1.0.0 -f ./EmsisoftTest.Processor/Dockerfile .
	docker tag emsisoft-test-processor:1.0.0 emsisoft-test-processor:latest

run: ## run services
	cd ./docker && docker-compose up -d

stop: ## stop services
	cd ./docker && docker-compose down