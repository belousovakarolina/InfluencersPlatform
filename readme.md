# Nuomonės formuotojų paieškos platforma

## Sistemos paskirtis:

Platformoje įmonės gali ieškoti tinkamų nuomonės formuotojų, skaitydami esamus atsiliepimus.

## Funkciniai reikalavimai:

* Įmonė gali palikti atsiliepimą
* Įmonė ir nuomonės formuotojai gali perskaityti atsiliepimą

## Technologijos:

* Loginiai komponentai realizuojami C# .NET karkasu
* Vaizdinė sąsaja realizuojama su JavaScript React biblioteka
* Loginiai ir vaizdiniai komponentai tarpusavyje bendrauja REST APIs

## Objektai:

* Nuomonės formuotojo profilis: primary key id, external id userId, string name, string description, int igFollowersCount, int fbFollowersCount, int tiktokFollowersCount, external id category. 
    * Galima grąžinti visus atsiliepimus apie šį nuomonės formuotoją.
* Įmonės profilis: primary key id, external id userId, string name, string description, float yearlyIncome
    * Galima grąžinti visus atsiliepimus, kuriuos parašė ši įmonė.
* Atsiliepimas: primary key id, external id influencerId, external id companyId, string description, int stars, boolean verified
* Kategorija: primary key id, string name. 
    * Čia skirstomi nuomonės formuotojai pagal dydį (maži, standartinio dydžio, dideli nuomonės formuotojai)

## Rolės:

* Rolės: Nuomonės formuotojas, Įmonė, Administratorius
* Visi useriai turi: primary key id, string username, string email, string password, boolean isDeleted

## OPENAPI dokumentacija

{
  "openapi": "3.0.1",
  "info": {
    "title": "InfluencersPlatformBackend",
    "version": "1.0"
  },
  "paths": {
    "/api/v1/category/{id}": {
      "get": {
        "tags": [
          "Category"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "put": {
        "tags": [
          "Category"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PutCategoryRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PutCategoryRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PutCategoryRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "patch": {
        "tags": [
          "Category"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchCategoryRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchCategoryRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PatchCategoryRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "delete": {
        "tags": [
          "Category"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/category": {
      "get": {
        "tags": [
          "Category"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "post": {
        "tags": [
          "Category"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateCategoryRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateCategoryRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateCategoryRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/companyprofile/{id}": {
      "get": {
        "tags": [
          "CompanyProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "put": {
        "tags": [
          "CompanyProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PutCompanyProfileRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PutCompanyProfileRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PutCompanyProfileRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "patch": {
        "tags": [
          "CompanyProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchCompanyProfileRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchCompanyProfileRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PatchCompanyProfileRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "delete": {
        "tags": [
          "CompanyProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/companyprofile": {
      "get": {
        "tags": [
          "CompanyProfile"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "post": {
        "tags": [
          "CompanyProfile"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateCompanyProfileRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateCompanyProfileRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateCompanyProfileRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/influencerprofile/{id}": {
      "get": {
        "tags": [
          "InfluencerProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "put": {
        "tags": [
          "InfluencerProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PutInfluencerProfileRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PutInfluencerProfileRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PutInfluencerProfileRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "patch": {
        "tags": [
          "InfluencerProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchInfluencerProfileRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchInfluencerProfileRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PatchInfluencerProfileRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "delete": {
        "tags": [
          "InfluencerProfile"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/influencerprofile": {
      "get": {
        "tags": [
          "InfluencerProfile"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "post": {
        "tags": [
          "InfluencerProfile"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateInfluencerProfileRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateInfluencerProfileRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateInfluencerProfileRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/review/{id}": {
      "get": {
        "tags": [
          "Review"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "put": {
        "tags": [
          "Review"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PutReviewRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PutReviewRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PutReviewRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "patch": {
        "tags": [
          "Review"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchReviewRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchReviewRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PatchReviewRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "delete": {
        "tags": [
          "Review"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/review": {
      "get": {
        "tags": [
          "Review"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "post": {
        "tags": [
          "Review"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateReviewRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateReviewRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateReviewRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/user/{id}": {
      "get": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "put": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PutUserRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PutUserRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PutUserRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "patch": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchUserRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/PatchUserRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/PatchUserRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "delete": {
        "tags": [
          "User"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/user": {
      "get": {
        "tags": [
          "User"
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "post": {
        "tags": [
          "User"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateUserRequestDTO"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateUserRequestDTO"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateUserRequestDTO"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "CreateCategoryRequestDTO": {
        "required": [
          "followersCountFrom",
          "followersCountTo",
          "name"
        ],
        "type": "object",
        "properties": {
          "name": {
            "minLength": 1,
            "type": "string"
          },
          "followersCountFrom": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32"
          },
          "followersCountTo": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32"
          }
        },
        "additionalProperties": false
      },
      "CreateCompanyProfileRequestDTO": {
        "required": [
          "name",
          "userId"
        ],
        "type": "object",
        "properties": {
          "userId": {
            "type": "integer",
            "format": "int32"
          },
          "name": {
            "minLength": 1,
            "type": "string"
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "yearlyIncome": {
            "type": "number",
            "format": "double",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "CreateInfluencerProfileRequestDTO": {
        "type": "object",
        "properties": {
          "userId": {
            "type": "integer",
            "format": "int32"
          },
          "categoryId": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "igFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "fbFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "tiktokFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "CreateReviewRequestDTO": {
        "type": "object",
        "properties": {
          "influencerId": {
            "type": "integer",
            "format": "int32"
          },
          "companyId": {
            "type": "integer",
            "format": "int32"
          },
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "stars": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "isAboutInfluencer": {
            "type": "boolean"
          }
        },
        "additionalProperties": false
      },
      "CreateUserRequestDTO": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "phone": {
            "type": "string",
            "nullable": true
          },
          "role": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PatchCategoryRequestDTO": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "followersCountFrom": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "followersCountTo": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PatchCompanyProfileRequestDTO": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "yearlyIncome": {
            "type": "number",
            "format": "double",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PatchInfluencerProfileRequestDTO": {
        "type": "object",
        "properties": {
          "categoryId": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "igFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "fbFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "tiktokFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PatchReviewRequestDTO": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "stars": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "verified": {
            "type": "boolean",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PatchUserRequestDTO": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "phone": {
            "type": "string",
            "nullable": true
          },
          "isDeleted": {
            "type": "boolean",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PutCategoryRequestDTO": {
        "required": [
          "followersCountFrom",
          "followersCountTo",
          "name"
        ],
        "type": "object",
        "properties": {
          "name": {
            "minLength": 1,
            "type": "string"
          },
          "followersCountFrom": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32"
          },
          "followersCountTo": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32"
          }
        },
        "additionalProperties": false
      },
      "PutCompanyProfileRequestDTO": {
        "required": [
          "name"
        ],
        "type": "object",
        "properties": {
          "name": {
            "minLength": 1,
            "type": "string"
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "yearlyIncome": {
            "type": "number",
            "format": "double",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PutInfluencerProfileRequestDTO": {
        "type": "object",
        "properties": {
          "categoryId": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "igFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "fbFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "tiktokFollowerCount": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PutReviewRequestDTO": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "stars": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "verified": {
            "type": "boolean"
          }
        },
        "additionalProperties": false
      },
      "PutUserRequestDTO": {
        "type": "object",
        "properties": {
          "name": {
            "type": "string",
            "nullable": true
          },
          "email": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          },
          "phone": {
            "type": "string",
            "nullable": true
          },
          "isDeleted": {
            "type": "boolean"
          }
        },
        "additionalProperties": false
      }
    }
  }
}